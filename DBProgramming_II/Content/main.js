function getProductDetails(productCode) {
    $.get('/Home/ProductDetails/' + productCode, function (data) {
        $('#exampleModal').modal('show');
        $(".modal-body").html(data);
    });
}

