const form = document.getElementById("createSubjectform");
const createButton = document.getElementById("createSubjectButton");
let subjectName = document.getElementById("Name");
let hoursPerWeek = document.getElementById("HoursPerWeek");

//showing an alert message when the form is submitted - create subject
createButton.addEventListener("click", () => {
    let subjectNameValue = subjectName.value;
    let hoursPerWeekValue = hoursPerWeek.value;

    if (checkName(subjectNameValue) && checkMaxHours(hoursPerWeekValue)) {
        setTimeout(() => {
            form.submit();
        }, 1700);

        Swal.fire({
            title: "Success!",
            text: "Subject '" + subjectNameValue + "' has been added!",
            icon: "success",
        });
    }
    else {
        form.submit();
    }
});