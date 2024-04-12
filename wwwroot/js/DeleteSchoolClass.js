﻿const form = document.getElementById("deleteClassform");
const submit = document.getElementById("deleteClassButton");
let classLetter = document.getElementById("ClassLetter");
let yearOfStudy = document.getElementById("YearOfStudy");

//showing an alert message when the form is submitted
submit.addEventListener("click", () => {
    let yearValue = yearOfStudy.options[yearOfStudy.selectedIndex].text;
    let classLetterValue = classLetter.options[classLetter.selectedIndex].text;

    alert("success!");

    if (classLetterValue.length == 1)
    {
        setTimeout(() => {
            form.submit();
        }, 1700);

        swal({
            title: "Success!",
            text: "Professor " + firstNameValue + " " + lastNameValue + " has been deleted!",
            icon: "success",
        });


        //swal({
        //    title: "Are you sure?",
        //    text: "Once deleted, you will not be able to recover the same class!",
        //    icon: "warning",
        //    buttons: true,
        //    dangerMode: true,
        //})
        //.then((willDelete) => {
        //    if (willDelete) {
        //        swal("The class " + yearValue + classLetterValue + " has been deleted!", {
        //            icon: "success",
        //        });
        //        setTimeout(() => {
        //            form.submit();
        //        }, 1700);
        //    }
        //    else {
        //        swal("The class was not deleted!");
        //    }
        //});
    }
});

//changing the class letter depending on user input for school year
yearOfStudy.addEventListener('change', changeLetter);
function changeLetter() {
    let yearValue = yearOfStudy.value;
    classLetter.value = yearValue;
}