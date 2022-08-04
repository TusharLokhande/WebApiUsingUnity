$(document).ready(() => {
 
    $.noConflict();
    $.ajax({
        'url': "http://localhost:50866/api/employee/get",
        'method': "GET",
        'contentType': 'application/json'
    }).done(function (data) {
        console.log(data);
        
        $('#table').dataTable({
            "data": data,
            "columns": [
                { "data": "EName" },
                { "data": "Email" },
                {
                    "data": (data) => {
                        var str = String(data.DateOfBirth);
                        var num = parseInt(str.replace(/[^0-9]/g, ""));
                        let d = new Date(num).toLocaleDateString("en-US");
                        console.log(d);
                        return d;
                    }
                },
                { "data": "DepartmentName" },
                { "data": "ReportingManagerName" },
                { "render": function (data, type, full, meta) { return `<a class="btn btn-primary" href="/home/Create/${full.Id}">Update</a>` } },
            ],
        });


    })
})

