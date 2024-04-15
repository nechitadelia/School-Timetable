//changing the cathegory of classes - click event on buttons
let categories = Array.from(document.getElementsByClassName("grd-button"));
let classTables = Array.from(document.getElementsByClassName("classes"));
let activeSchoolYear = classTables[0];
let colorIndex = 0;

for (let i = 0; i < categories.length; i++) {
    categories[i].addEventListener("click", () => {
        setTimeout(() => {
            if (activeSchoolYear != classTables[i]) {
                classTables[i].classList.remove("hidden-elem");
                activeSchoolYear.classList.add("hidden-elem");
                activeSchoolYear = classTables[i];

                categories[i].classList.remove("darken-button");
                categories[colorIndex].classList.add("darken-button");
                colorIndex = i;
            }
        }, 50);

    });
}