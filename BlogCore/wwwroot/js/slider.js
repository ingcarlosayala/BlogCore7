
var datatable;

$(document).ready(function () {
    datatable = $("#tblslider").DataTable({
        "ajax": {
            "url": "/Admin/Sliders/ObtenerTodos"
        },
        "columns":[
            { "data": "nombre", "width": "40%" },
            {
                "data": "estado",
                "render": function (data) {
                    if (data == true) {
                        return "Activo";
                    } else {
                        return "Inactivo";
                    }
                },"Width": "40%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/Sliders/Edit/${data}" class="btn btn-warning btn-sm"> <i class="fa-regular fa-pen-to-square"></i></a>
                                <a onclick=Delete("/Admin/Sliders/Delete/${data}") class="btn btn-danger btn-sm"> <i class="fa-solid fa-trash"></i></a>
                            </div>`;
                },"width": "40%"
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