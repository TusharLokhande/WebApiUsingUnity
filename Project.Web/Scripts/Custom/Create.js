$(document).ready(() => {
    

   


    //DropDowns 
    DropDown('dropdown', '#DepartmentId');
    DropDown('reportingmanager', '#ReportingManagerId');

    
   

    //Post Calling
    $("#submit").click((e) => {
        e.preventDefault();
        let Id = $("#Id").val();
        let EName = $("#EName").val();
        let Email = $("#Email").val();
        let DateOfBirth = $("#DateOfBirth").val();
        let DepartmentId = $("#DepartmentId").val();
        let ReportingManagerId = $("#ReportingManagerId").val();
        let data = { Id, EName, Email, DateOfBirth, DepartmentId, ReportingManagerId }
        console.log(data);
        Post(data);
    })
    
})

function DropDown(name, id) {
    $.ajax({
        type: 'GET',
        dataType: "json",
        url: `http://localhost:50866/api/employee/${name}`,
        success: (data) =>{
            let i = '';
            data.map(data => { i = i + `<option value=${data.Id}>${data.Name}</option>`})
            $(id).html(i);
        }
    });
}



function Post (obj)  {
    $.ajax({
            type: "POST",
            url: `http://localhost:50866/api/employee/Post/`,
            data: obj,
            success: function (data) {
                console.log(data);
                window.location.href = '/Home/dashboard';
            }
        });
}

