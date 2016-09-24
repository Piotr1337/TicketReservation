$(".chosen").chosen({ max_selected_options: 1 });
$(".chosen-deselect").chosen({ allow_single_deselect: true });
$(".chosen").chosen().change();
$(".chosen").trigger('liszt:updated');