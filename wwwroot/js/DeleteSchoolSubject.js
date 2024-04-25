const form = document.getElementById("deleteSubjectform");
const deleteButton = document.getElementById("deleteSubjectButton");
let subjectName = document.getElementById("Name");
let spanError = document.getElementById("span-error");

//showing an alert message when the form is submitted - delete professor
deleteButton.addEventListener("click", () => {
    let subjectNameValue = subjectName.value;

    if (spanError == null) {
        setTimeout(() => {
            form.submit();
        }, 1700);

        Swal.fire({
            title: "Success!",
            text: "Subject '" + subjectNameValue + "' has been deleted!",
            icon: "success",
        });
    }
    else {
        form.submit();
    }
});