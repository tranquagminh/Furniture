var multipleCardCarousel2 = document.querySelector(
    "#main_slider"
);
if (window.matchMedia("(min-width: 768px)").matches) {
    var carousel = new bootstrap.Carousel(multipleCardCarousel2, {
        interval: false,
    });
    var carouselWidth = $(".carousel-inner")[0].scrollWidth;
    var cardWidth = $(".carousel-item").width();
    var scrollPosition2 = 0;
    $("#main_slider .carousel-control-next").on("click", function () {
        if (scrollPosition2 < carouselWidth - cardWidth * 4) {
            scrollPosition2 += cardWidth;
            $("#main_slider .carousel-inner").animate(
                { scrollLeft: scrollPosition2 },
                600
            );
        }
    });
    $("#main_slider .carousel-control-prev").on("click", function () {
        if (scrollPosition2 > 0) {
            scrollPosition2 -= cardWidth;
            $("#main_slider .carousel-inner").animate(
                { scrollLeft: scrollPosition2 },
                600
            );
        }
    });
} else {
    $(multipleCardCarousel2).addClass("slide");
}