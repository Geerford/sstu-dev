$(function getItemData() {
    $.ajaxSetup({ cache: false });
    $(".item").click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#content').html(data);
            $('#dialog').modal('show');
        });
    });
})