$(".chosen").chosen({
    max_selected_options: 3, width: "100%", placeholder_text_multiple: " ",
    no_results_text: "Nic nie znaleziono"
});
$(".chosen-deselect").chosen({ allow_single_deselect: true });
$(".chosen").chosen().change();
$(".chosen").trigger('liszt:updated');