function AbrirModalExcluir(id) {
    $("#Id").val(id);

    var myModal = new bootstrap.Modal(document.getElementById("exampleModal"), {});
    myModal.show();
}

function Remover() {
    $.post("Excluir/" + $("#Id").val(),
        function (data) {
            window.location.replace("https://localhost:7197/Teste/NovaTela");
        })
}