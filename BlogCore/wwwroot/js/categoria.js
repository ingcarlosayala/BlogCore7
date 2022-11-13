
var datatable;

$(document).ready(function () {
    datatable = $("#tblcategoria").DataTable({
        "ajax": {
            "url": "/Admin/Categorias/ObtenerTodos"
        },
        "columns":[
            {"data": "nombre", "width": "30%"},
            { "data": "orden", "width": "30%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/Categorias/Edit/${data}" class="btn btn-warning btn-sm"> <i class="fa-regular fa-pen-to-square"></i></a>
                                <a onclick=Delete("/Admin/Categorias/Delete/${data}") class="btn btn-danger btn-sm"> <i class="fa-solid fa-trash"></i></a>
                            </div>`;
                },"width": "20%"
            }
        ]
    });
});

function Delete(url) {

    Swal.fire({
        title: 'Estas Seguro?',
        text: "Este Archivo no se podra revertir!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data) {
                        toastr.success(data.message);
                        datatable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    })
}