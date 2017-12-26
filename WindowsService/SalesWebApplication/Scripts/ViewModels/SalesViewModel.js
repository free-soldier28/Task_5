var SalesViewModel = function ()
{
    var self = this;

    this.Sales = ko.observableArray([]);
    this.Product = ko.observableArray([]);

    this.AddSalesModel = new AddSalesViewModel();

    this.showAddSalesForm = function () {
        self.AddSalesModel.IsActive(!self.AddSalesModel.IsActive());
    }

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

    this.deleteSales = function (Id) {
        $.ajax({
            type: 'POST',
            url: '/Home/DeleteByIdSales',
            data: ko.toJSON(Id),
            success: function (data) {

            },
            contentType: "application/json",
            dataType: 'json'
        });
        self.Sales.remove(this);
    }

    var loadDataDiagramm = function () {
        $.ajax({
            url: '/Home/GetProductSales',
            type: "GET",
            success: function (data) {
                self.Product(data);
            }
        });
    }

    loadSales();
    //loadDataDiagramm();
};
