var AddSalesViewModel = function ()
{
    var self = this;

    //this.Date = ko.observable().extend({ required: true });
    this.ManagerName = ko.observable().extend({ required: true, minLength: 2, maxLength: 50 });
    this.CustomerName = ko.observable(jsonModel.CustomerName).extend({ required: true, minLength: 2, maxLength: 50 });
    this.ProductName = ko.observable(jsonModel.ProductName).extend({ required: true, minLength: 2, maxLength: 50 });
    this.Amount = ko.observable(jsonModel.Amount).extend({ required: true });

    this.IsActive = ko.observable(false);

    this.ok = function ()
    {
        if (self.errors().length === 0)
        {
            $.ajax({
                type: 'POST',
                url: '/Home/AddSales',
                data: ko.toJSON(self),
                success: function (data)
                {
                    self.IsActive(false);
                },
                contentType: "application/json",
                dataType: 'json'
            });
        }
        else
        {
            self.errors.showAllMessages();
        }
    }

    this.cancel = function ()
    {
        self.IsActive(false);
    }

    this.errors = ko.validation.group(self);
}