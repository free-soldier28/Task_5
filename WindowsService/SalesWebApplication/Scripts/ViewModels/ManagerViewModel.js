var ManagerViewModel = function ()
{
    var self = this;

    this.Id = ko.observable();
    this.ManagerName = ko.observable();
    this.Managers = ko.observableArray([]);

    var getManagers = function ()
    {
        $.ajax({
            type: "Post",
            url: '/Manager/GetManagers',
            success: function (result)
            {
                self.Managers(result);
            }
        });
    }
    getManagers();

    this.addManager = function ()
    {
        $.ajax({
            type: "Post",
            url: '/Manager/AddManager',
            data: ko.toJSON(self),
            success: function (id)
            {
                self.Managers.unshift(
                {
                    Id: id,
                    SecondName: self.ManagerName()
                });
                self.clearForm();
            },
            contentType: "application/json",
            dataType: 'json'
        });
    }

    this.loadManagerEditForm = function (context)
    {
        var id = context.Id;
        var objManager = ko.utils.arrayFirst(self.Managers(),
            function (item)
            {
                return item.Id === id;
            });
        self.Id(objManager.Id);
        self.ManagerName(objManager.SecondName);
    }

    this.editManager = function (content)
    {
        var Obj = function () {
            this.Id = self.Id();
            this.SecondName = self.ManagerName();
        }

        $.ajax({
            type: 'POST',
            url: '/Manager/EditManager',
            data: ko.toJSON(new Obj),
            success: function ()
            {

            },
            contentType: "application/json",
            dataType: 'json'
        });

        var objManager = ko.utils.arrayFirst(self.Managers(), function (item) {
            return item.Id === self.Id();
        });
        self.Managers.replace(objManager, new Obj);

        self.clearForm();
    }

    this.deleteManager = function (content)
    {
        self.Id(content.Id);

        $.ajax({
            type: "Post",
            url: '/Manager/DeleteManager',
            data: ko.toJSON(self),
            success: function ()
            {
                
            },
            contentType: "application/json",
            dataType: 'json'
        });
        self.Managers.remove(this);
    }

    this.clearForm = function () {
        self.Id(null);
        self.ManagerName(null);
        $('#AddFormModal').modal('hide');
        $('#EditFormModal').modal('hide');
    }
}