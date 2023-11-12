﻿var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#productTable').DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAll"
        },
        "columns": [
            { "data": "title" },
            { "data": "isbn" },
            { "data": "price" },
            { "data": "author" },
            { "data": "category.name" },
            { "data": "coverType.name" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="btn-group w-75">
                       <a href="/Admin/Product/Upsert?id=${data}" class="mx-2 btn btn-success"> <i class="bi bi-pen"></i>  Edit </a>
                         &nbsp;
                        <a  onClick=Delete('/Admin/Product/Delete/${data}') class="mx-2 btn btn-danger"> <i class="bi bi-trash3"></i> Delete </a>
                        </div>
                        `
                }
            },




        ]

    });



}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        // toastr.success(data.message);
                    }
                    else {
                        //  toastr.error(data.message);

                    }
                }

            })
        }
    })
}