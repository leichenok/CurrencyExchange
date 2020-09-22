var Site = {
    calculate: function ()
    {
        var currencyCode = $("#ddlCurrencyCodes").val();
        var amount = $("#txtFrom").val();

        if (amount < 0) {
            alert('Amount should be more than 0');
            $('#txtConvertedAmount').text();
            return;
        }

        $.ajax({
            url: "/Home/Calculate",
            method: "POST",
            data: { code: currencyCode, amount: amount }
        })
        .done(function (response) {
            if (response.Success == true) {
                $('#txtConvertedAmount').text(response.Amount);
        }
        else {
            alert('Failed');
        }
        })
        .fail(function () {
            alert("Error");
        });
    }
}