var tabla = {};
$(document).ready(function () {
    tabla = $('#dtlista').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
        },
        "order": [[0, "desc"]]
    });
    sumar();
});
function orden(id) {
    window.location.href = `/Home/Pendiente/${id}`;
}
function sumar() {
    var total = 0;
    tabla.rows().data().each(function (el, index) {
        total = eval(total) + eval(el[4]);
    });
    $("#total").val(total);
}
$(document).on("submit", "#confirma", function (e) {
    e.preventDefault();
    alertify.confirm("Confirmar Pedido", "COnfirmar Ahora", function () {
        var id = $("#OrderID").val();
        $.post(`/Home/Confirma/${id}`, $("#confirma").serialize(), function (res) {
            alertify.success(res.msg);
        });
    }, function () {

    });
});