$(document).ready(function () {
    var currentUrl = window.location.href;
    $('.nav-treeview a').each(function () {
        if (currentUrl.indexOf($(this).attr('href')) > -1) {
            $(this).addClass('active');
        }
    });

    var t = $("#tbProducts").DataTable({
        "paging": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
        "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"],
        "ajax": {
            url: "https://localhost:7173/api/Product",
            type: "GET",
            "datatype": "json",
            "dataSrc": "data"
        },
        "columns": [
            { "data": "productId" },
            { "data": "name" },
            { "data": "price" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `<button type="button" class="btn btn-warning btn-sm edit-button" data-toggle="modal" data-tooltip = "tooltip" data-placement="right" title="Edit Data" data-target="#productModal" onclick="setEditProductModal('${data.name}', '${data.price}', '${data.productId}')"><i class="fas fa-edit"></i> </button> 
                        <button type="button" class="btn btn-danger btn-sm remove-button" data-toggle="modal" data-tooltip = "tooltip" data-placement="right" title="Hapus Data" onclick="Delete('${data.productId}')"><i class="fas fa-trash"></i> </button>`;
                },
                "orderable": false
            }
        ]
    });
    t.on('order.dt search.dt', function () {
        t.column(0, {
            search: 'applied',
            order: 'applied'
        }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
})

const setAddProductModal = () => {
    $('#modalLabel').text("Tambahkan data produk") // ganti modal title
    $('#addProduct').addClass("d-block") // menampilkan form untuk menambahkan data
    $('#editProduct').removeClass("d-block") // menyembunyikan form edit
    return false;
}

const setEditProductModal = (name, price, productId) => {
    $('#modalLabel').text("Edit data penjualan") // ganti modal title
    $('#editProduct').addClass("d-block") // menampilkan form untuk mengedit data
    $('#addProduct').removeClass("d-block") // menyembunyikan form tambah

    $('#editProductName').val(name)
    $('#editProductPrice').val(price)
    $('#editProductId').val(productId) // menampilkan data sebelumnya
    return false;
}

function save(e) {
    e.preventDefault()
    var product = new Object()
    product.name = $('#inputProductName').val()
    product.price = $('#inputProductPrice').val()
    if (product.name == '' || product.price == '' || null) {
        Swal.fire({
            icon: 'error',
            title: 'Error!',
            text: 'Kolom tidak boleh kosong!'
        })
        $('#productModal').modal('hide');
    } else {
        $.ajax({
            type: 'POST',
            url: 'https://localhost:7173/api/Product/',
            data: JSON.stringify(product),
            contentType: "application/json; charset=utf-8"
        }).then((result) => {
            $('#tbProducts').DataTable().ajax.reload();
            if (result.status_code == 200) {
                Swal.fire(
                    'Berhasil',
                    result.message,
                    'success'
                )
            }
            else {
                Swal.fire({
                    icon: 'error',
                    title: 'Error!',
                    text: result.message
                })
            }
        })
        $('#productModal').modal('hide');
    }
}

function Update(e) {
    e.preventDefault()
    var product = new Object();
    product.productId = $('#editProductId').val();
    product.name = $('#editProductName').val();
    product.price = $('#editProductPrice').val();
    if (product.name == '' || product.price == '' || null) {
        Swal.fire({
            icon: 'error',
            title: 'Error!',
            text: 'Kolom tidak boleh kosong!'
        })
        $('#saleModal').modal('hide');
    } else {
        $.ajax({
            url: "https://localhost:7173/api/Product/",
            type: "PUT",
            data: JSON.stringify(product),
            contentType: "application/json; charset=utf-8"
        }).then((result) => {
            $('#tbProducts').DataTable().ajax.reload();
            if (result.status_code == 200) {
                Swal.fire(
                    'Berhasil',
                    result.message,
                    'success'
                )
            }
            else {
                Swal.fire({
                    icon: 'error',
                    title: 'Error!',
                    text: result.message
                })
            }
        })
        $('#productModal').modal('hide');
    }
}
function Delete(productId) {
    Swal.fire({
        title: 'Apakah anda yakin?',
        text: "Data anda tidak bisa dikembalikan!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ya, hapus saja.'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "https://localhost:7173/api/Product/" + productId,
                type: "DELETE",
                dataType: "json"
            }).then((result) => {
                $('#tbProducts').DataTable().ajax.reload();
                if (result.status_code == 200) {
                    Swal.fire(
                        'Berhasil',
                        result.message,
                        'success'
                    )
                }
                else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error!',
                        text: result.message
                    })
                }
            });
        }
    })
    $('#departmentModal').modal('hide');
}
$(function () {
    $('[data-tooltip="tooltip"]').tooltip({
        trigger: 'hover'
    })
});

$('body').tooltip({
    selector: '[data-tooltip="tooltip"]',
    container: 'body',
    trigger: 'hover'
});