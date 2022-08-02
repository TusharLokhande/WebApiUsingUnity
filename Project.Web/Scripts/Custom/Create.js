$(document).ready(() => {
    DropDown('dropdown', '#DepartmentId');
    DropDown('reportingmanager', '#ReportingManagerId');
})

function DropDown(name, id) {
    $.ajax({
        type: 'GET',
        dataType: "json",
        url: `http://localhost:50866/api/employee/${name}`,
        success: function (data, status, xhr) {
            let i = '';
            data.map(data => { i = i + `<option value=${data.Id}>${data.Name}</option>`})
            console.log("done");
            $(id).html(i);
        }
    });
}