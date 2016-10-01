$(document).ready(function() {
    $('#filer_input').filer({
        limit: 1,
        maxSize: null,
        extensions: null,
        changeInput:
            '<div class="jFiler-input-dragDrop">' +
                '<div class="jFiler-input-inner">' +
                '<div class="jFiler-input-icon">' +
                '<i class="icon-jfi-cloud-up-o"></i>' +
                '</div><div class="jFiler-input-text">' +
                '<h3>Przeciągnij zdjęcie tutaj</h3> ' +
                '<span style="display:inline-block; margin: 15px 0">lub</span></div>' +
                '<a class="jFiler-input-choose-btn blue">Przeglądaj...</a>' +
                '</div>' +
            '</div>',
        showThumbs: true,
        theme: "dragdropbox",
        dragDrop:
        {
            dragEnter: null, //A function that is fired when a dragged element enters the input. {Function}
            dragLeave: null, //A function that is fired when a dragged element leaves the input. {Function}
            drop: null, //A function that is fired when a dragged element is dropped on a valid drop target. {Function}
            dragContainer: null //Drag container {String}
        },
        templates: {
            box: '<ul class="jFiler-items-list jFiler-items-grid"></ul>',
            item: '<li class="jFiler-item">\
						<div class="jFiler-item-container">\
							<div class="jFiler-item-inner">\
								<div class="jFiler-item-thumb">\
									<div class="jFiler-item-status"></div>\
									<div class="jFiler-item-thumb-overlay">\
										<div class="jFiler-item-info">\
											<div style="display:table-cell;vertical-align: middle;">\
												<span class="jFiler-item-title"><b title="{{fi-name}}">{{fi-name}}</b></span>\
												<span class="jFiler-item-others">{{fi-size2}}</span>\
											</div>\
										</div>\
									</div>\
									{{fi-image}}\
								</div>\
								<div class="jFiler-item-assets jFiler-row">\
									<ul class="list-inline pull-left">\
										<li>{{fi-progressBar}}</li>\
									</ul>\
									<ul class="list-inline pull-right">\
										<li><a class="icon-jfi-trash jFiler-item-trash-action"></a></li>\
									</ul>\
								</div>\
							</div>\
						</div>\
					</li>',
            itemAppend: '<li class="jFiler-item">\
							<div class="jFiler-item-container">\
								<div class="jFiler-item-inner">\
									<div class="jFiler-item-thumb">\
										<div class="jFiler-item-status"></div>\
										<div class="jFiler-item-thumb-overlay">\
											<div class="jFiler-item-info">\
												<div style="display:table-cell;vertical-align: middle;">\
													<span class="jFiler-item-title"><b title="{{fi-name}}">{{fi-name}}</b></span>\
													<span class="jFiler-item-others">{{fi-size2}}</span>\
												</div>\
											</div>\
										</div>\
										{{fi-image}}\
									</div>\
									<div class="jFiler-item-assets jFiler-row">\
										<ul class="list-inline pull-left">\
											<li><span class="jFiler-item-others">{{fi-icon}}</span></li>\
										</ul>\
										<ul class="list-inline pull-right">\
											<li><a class="icon-jfi-trash jFiler-item-trash-action"></a></li>\
										</ul>\
									</div>\
								</div>\
							</div>\
						</li>',
            progressBar: '<div class="bar"></div>',
            itemAppendToEnd: false,
            canvasImage: true,
            removeConfirmation: true,
            _selectors: {
                list: '.jFiler-items-list',
                item: '.jFiler-item',
                progressBar: '.bar',
                remove: '.jFiler-item-trash-action'
            }
        },
        uploadFile: {
            url: "Admin/ArtistEdit",
            data: null,
            type: 'POST',
            enctype: 'multipart/form-data',
            synchron: true,
            beforeSend: function () { },
            success: function (data, itemEl, listEl, boxEl, newInputEl, inputEl, id) {
                var parent = itemEl.find(".jFiler-jProgressBar").parent(),
					new_file_name = JSON.parse(data),
					filerKit = inputEl.prop("jFiler");

                filerKit.files_list[id].name = new_file_name;

                itemEl.find(".jFiler-jProgressBar").fadeOut("slow", function () {
                    $("<div class=\"jFiler-item-others text-success\"><i class=\"icon-jfi-check-circle\"></i> Success</div>").hide().appendTo(parent).fadeIn("slow");
                });
            },
            error: function (el) {
                var parent = el.find(".jFiler-jProgressBar").parent();
                el.find(".jFiler-jProgressBar").fadeOut("slow", function () {
                    $("<div class=\"jFiler-item-others text-error\"><i class=\"icon-jfi-minus-circle\"></i> Error</div>").hide().appendTo(parent).fadeIn("slow");
                });
            },
        },
        addMore: false,
        allowDuplicates: true,
        clipBoardPaste: true,
        onEmpty: null,
        options: null,
        dialogs: {
            swal: function (text) {
                return swal(
                {
                    title: "Jesteś pewny?",
                    text: text,
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "Tak, usuń!",
                    cancelButtonText: "Anuluj",
                    closeOnConfirm: false
                });
            },
            confirm: function (text, callback) {
                confirm(text) ? callback() : null;
            }
        },
        captions: {
            button: "Choose Files",
            feedback: "Choose files To Upload",
            feedback2: "files were chosen",
            drop: "Drop file here to Upload",
            removeConfirmation: "Are you sure you want to remove this file?",
            errors: {
                filesLimit: "Only {{fi-limit}} files are allowed to be uploaded.",
                filesType: "Only Images are allowed to be uploaded.",
                filesSize: "{{fi-name}} is too large! Please upload file up to {{fi-maxSize}} MB.",
                filesSizeAll: "Files you've choosed are too large! Please upload files up to {{fi-maxSize}} MB."
            }
        }
    });
});