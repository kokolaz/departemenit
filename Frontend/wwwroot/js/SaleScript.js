$(document).ready(function () {
    var currentUrl = window.location.href;
    $('.nav-treeview a').each(function () {
        if (currentUrl.indexOf($(this).attr('href')) > -1) {
            $(this).addClass('active');
        }
    });

    var t = $("#tbSales").DataTable({
        "paging": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
        "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"],
        "ajax": {
            url: "https://localhost:7173/api/Sale",
            type: "GET",
            "datatype": "json",
            "dataSrc": "data"
        },
        "columns": [
            { "data": "saleId" },
            { "data": "product.name" },
            { "data": "quantity" },
            { "data": "product.price" },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `<button type="button" class="btn btn-warning btn-sm edit-button" data-toggle="modal" data-tooltip = "tooltip" data-placement="right" title="Edit Data" data-target="#saleModal" onclick="setEditSaleModal('${data.productId}', '${data.quantity}', '${data.saleId }')"><i class="fas fa-edit"></i> </button> 
                        <button type="button" class="btn btn-danger btn-sm remove-button" data-toggle="modal" data-tooltip = "tooltip" data-placement="right" title="Hapus Data" onclick="Delete('${ data.saleId }')"><i class="fas fa-trash"></i> </button>`;
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

const setAddSaleModal = () => {
    $('#modalLabel').text("Tambahkan data penjualan") // ganti modal title
    $('#addSale').addClass("d-block") // menampilkan form untuk menambahkan data
    $('#editSale').removeClass("d-block") // menyembunyikan form edit
    return false;
}

const setEditSaleModal = (productId, quantity, saleId) => {
    $('#modalLabel').text("Edit data penjualan") // ganti modal title
    $('#editSale').addClass("d-block") // menampilkan form untuk mengedit data
    $('#addSale').removeClass("d-block") // menyembunyikan form tambah

    $('#editProductId').val(productId)
    $('#editProductQuantity').val(quantity)
    $('#editSaleId').val(saleId) // menampilkan data sebelumnya
    return false;
}

function save(e) {
    e.preventDefault()
    var sale = new Object()
    sale.productId = $('#addProductId').val()
    sale.quantity = $('#addQuantity').val()
    if (sale.productId == '' || sale.quantity == '' || null) {
        Swal.fire({
            icon: 'error',
            title: 'Error!',
            text: 'Kolom tidak boleh kosong!'
        })
        $('#departmentModal').modal('hide');
    } else {
        $.ajax({
            type: 'POST',
            url: 'https://localhost:7173/api/Sale/',
            data: JSON.stringify(sale),
            contentType: "application/json; charset=utf-8"
        }).then((result) => {
            $('#tbSales').DataTable().ajax.reload();
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
        $('#saleModal').modal('hide');
    }
}

function Update(e) {
    e.preventDefault()
    var sale = new Object();
    sale.saleId = $('#editSaleId').val();
    sale.productId = $('#editProductId').val();
    sale.quantity = $('#editProductQuantity').val();
    if (sale.productId == '' || sale.quantity == '' || null) {
        Swal.fire({
            icon: 'error',
            title: 'Error!',
            text: 'Kolom tidak boleh kosong!'
        })
        $('#saleModal').modal('hide');
    } else {
        $.ajax({
            url: "https://localhost:7173/api/Sale/",
            type: "PUT",
            data: JSON.stringify(sale),
            contentType: "application/json; charset=utf-8"
        }).then((result) => {
            $('#tbSales').DataTable().ajax.reload();
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
        $('#saleModal').modal('hide');
    }
}
function Delete(saleId) {
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
                url: "https://localhost:7173/api/Sale/" + saleId,
                type: "DELETE",
                dataType: "json"
            }).then((result) => {
                $('#tbSales').DataTable().ajax.reload();
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
    $('#saleModal').modal('hide');
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