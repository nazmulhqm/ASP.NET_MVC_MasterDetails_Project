var Categories = []

function LoadCategory(element) {
    if (Categories.length == 0) {
        $.ajax({
            type: "GET",
            url: '/Order/getProductCategories',
            success: function (data) {
                Categories = data;
                renderCategory(element);
            }
        })
    }
    else {
        renderCategory(element);
    }
}

function renderCategory(element) {
    var $ele = $(element);
    $ele.empty()
    $ele.append($('<option/>').val('0').text('Select'));
    $.each(Categories, function (i, val) {
        $ele.append($('<option/>').val(val.CategoryId).text(val.CategoryName));
    })
}

function LoadProducts(categoryDD) {
    $.ajax({
        type: "GET",
        url: '/Order/getProducts',
        data: { 'categoryId': $(categoryDD).val() },
        success: function (data) {
            renderProduct($(categoryDD).parents('.mycontainer').find('select.product'), data);
        },
        error: function (error) {
            console.log(error)
        }
    })
}

function renderProduct(element, data) {
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select'));

    $('#rate').val('');

    $.each(data, function (i, val) {
        var $option = $('<option/>').val(val.ProductId).text(val.ProductName);
        $option.data('unit-price', val.UnitPrice); 
        $ele.append($option);
    });

    $ele.on('change', function () {
        var selectedProductId = $(this).val();
        if (selectedProductId !== '0') {
            var unitPrice = $(this).find(':selected').data('unit-price');
            $('#rate').val(unitPrice);
        } else {
            $('#rate').val('');
        }
    });
}

LoadCategory($('#productCategory'))

$(document).ready(function () {
    //Image Section
    var formData = new FormData();
    formData.append('file', $('#imageupload')/*[0].file[0]*/);

    //add items
    $("#add").click(function () {
        var isAllValid = true;

        if ($('#productCategory').val() == '0') {
            isAllValid = false;
            $('#productCategory').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#productCategory').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#product').val() == '0') {
            isAllValid = false;
            $('#product').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#product').siblings('span.error').css('visibility', 'hidden');
        }

        if (!$('#quantity').val().trim() != "" && (parseInt($('#quantity').val()) || 0)) {
            isAllValid = false;
            $('#quantity').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#quantity').siblings('span.error').css('visibility', 'hidden');
        }
        if (!$('#rate').val().trim() != "" && (!isNaN($('#rate').val().trim()))) {
            isAllValid = false;
            $('#rate').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#rate').siblings('span.error').css('visibility', 'hidden');
        }



        if (isAllValid) {
            var $newRow = $('#mainrow').clone().removeAttr('id');
            $('.productCategory', $newRow).val($('#productCategory').val());
            $('.product', $newRow).val($('#product').val());
            $('#add', $newRow).addClass('remove').val('Remove').removeClass('btn-success').addClass('btn-danger');
            $('#productCategory,#product,#quantity,#rate,#add', $newRow).removeAttr('id');
            $('span.error', $newRow).remove();

            $('#orderDetailsItems').append($newRow);

            $('#productCategory,#product').val('0');

            $('#quantity,#rate').val('');

            $('#orderItemError').empty();


        }
    })

    //remove Items
    $('#orderDetailsItems').on('click', '.remove', function () {
        $(this).parents('tr').remove();
    });

    //place Order
    $('#submit').click(function () {
        var isAllValid = true;
        $('#orderItemError').text('');
        var list = []
        var errorItemCount = 0;
        //item add to list
        $('#orderDetailsItems tbody tr').each(function (index, ele) {
            if (
                $('select.product', this).val() == "0" ||
                (parseInt($('.quantity', this).val()) ||
                    0) == 0 || $('.rate', this).val() == "" ||
                isNaN($('.rate', this).val())
            ) {
                errorItemCount++;
                $(this).addClass('error');
            }
            else {
                var orderItem = {
                    ProductID: $('select.product', this).val(),
                    Quantity: parseInt($('.quantity', this).val()),
                    UnitPrice: parseFloat($('.rate', this).val())
                }
                list.push(orderItem);
            }
        })
        //error count and validation
        if (errorItemCount > 0) {
            $('#orderItemError').text(errorItemCount + " Invalid entry in order item list!!!");
            isAllValid = false;
        }
        //No. of Item check
        if (list.length == 0) {
            $('#orderItemError').text(" At least one entry is required to order an item!!!");
            isAllValid = false;
        }
        //date get
        if ($('#orderDate').val().trim() == '') {
            $('#orderDate').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#orderDate').siblings('span.error').css('visibility', 'hidden');
        }

        //order submit
        if (isAllValid) {
            var customer = {
                CustomerName: $('#customerName').val().trim(),
                Gender: $('input[name="gender"]:checked').val(),
                Addresses: $('#address').val().trim(),
                Phone: $('#phone').val().trim(),
                Email: $('#email').val().trim(),
                Image: $('#imageupload').val().trim()
            };

            var order = {
                OrderDate: $('#orderDate').val().trim(),
                Note: $('#note').val().trim(),
                OrderDetails: list
            };

            $(this).val('Please Wait................')
            $.ajax({
                type: "POST",
                url: '/Order/Save',
                data: JSON.stringify({ customer: customer, order: order }),
                contentType: 'application/json',
                success: function (data) {
                    if (data.status) {
                        alert("Order Placed successfully!!!!!!!!!!!")
                        list = [];
                        $('#orderDate,#note,#imageupload,#bool,#customerName,#address,#phone,#email').val('');
                        $('#orderDetailsItems').empty();
                        //parsed order
                        //window.location = '/Order/List';
                    }
                    else {
                        alert('********ERROR********')
                    }
                    $('#submit').val('Save');
                },
                error: function (error) {
                    console.log(error)
                    $('#submit').val('Save');
                }
            })
        }
    });
});