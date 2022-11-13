
var datatable;

$(document).ready(function () {
    datatable = $("#tblproducto").DataTable({
        "ajax": {
            "url": "/Admin/Productos/ObtenerTodos"
        },
        "columns":[
            {"data": "nombre", "width": "15%"},
            { "data": "descripcion", "width": "15%" },
            { "data": "categoria.nombre", "width": "15%" },
            { "data": "fechaCreacion", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/Productos/Edit/${data}" class="btn btn-warning btn-sm"> <i class="fa-regular fa-pen-to-square"></i></a>
                                <a onclick=Delete("/Admin/Productos/Delete/${data}") class="btn btn-danger btn-sm"> <i class="fa-solid fa-trash"></i></a>
                            </div>`;
                },"width": "15%"
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