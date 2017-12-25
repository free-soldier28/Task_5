var SalesViewModel = function ()
{
    var self = this;

    this.Sales = ko.observableArray([]);

    //this.showAddSalesForm = function () {
    //    self.IsActive(!self.IsActive());
    //}
    this.IsActive = ko.observable(false);

    var loadSales = function ()
    {
        $.ajax({
            url: '/Home/GetSales',
            type: "GET",
            success: function (data)
            {
                self.Sales(data);
            }
        });
    }

    loadSales();
};
