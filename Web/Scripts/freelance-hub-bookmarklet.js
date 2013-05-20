
(function () {

    // the minimum version of jQuery we want
    var v = "1.3.2";

    // check prior inclusion and version
    if (window.jQuery === undefined || window.jQuery.fn.jquery < v) {
        var done = false;
        var script = document.createElement("script");
        script.src = "http://ajax.googleapis.com/ajax/libs/jquery/" + v + "/jquery.min.js";
        script.onload = script.onreadystatechange = function () {
            if (!done && (!this.readyState || this.readyState == "loaded" || this.readyState == "complete")) {
                done = true;
                initMyBookmarklet();
            }
        };
        document.getElementsByTagName("head")[0].appendChild(script);
    } else {
        initMyBookmarklet();
    }

    function initMyBookmarklet() {
        (window.freelanceBookmarklet = function () {
            if ($("#freelance_hub_frame").length == 0) {
                var title = encodeURIComponent(document.title);
                var url = encodeURIComponent(document.location);

                // Try to get description
                var description = $('meta[name="description"]').attr('content');
                description = encodeURIComponent(description);

                if ((title != "") && (title != null)) {
                    $("body").append("\
					<div id='freelance_hub_frame'>\
						<div id='fh_frame_veil' style=''>\
							<p>Loading...</p>\
						</div>\
						<iframe src='http://freelancehub.apphb.com/Jobs/SaveExternalJob?&title=" + title + "&url=" + url + "&description=" + description + "' onload=\"$('#freelance_hub_frame iframe').slideDown(500);\">Enable iFrames.</iframe>\
						<style type='text/css'>\
							#fh_frame_veil { display: none; position: fixed; width: 100%; height: 100%; top: 0; left: 0; background-color: rgba(255,255,255,.25); cursor: pointer; z-index: 900; }\
							#fh_frame_veil p { color: black; font: normal normal bold 20px/20px Helvetica, sans-serif; position: absolute; top: 50%; left: 50%; width: 10em; margin: -10px auto 0 -5em; text-align: center; }\
							#freelance_hub_frame iframe { display: none; position: fixed; top: 5%; right: 5%; width: 30%; height: 60%; z-index: 999; border: 10px solid rgba(0,0,0,.5); margin: -5px 0 0 -5px; }\
						</style>\
					</div>");
                    $("#fh_frame_veil").fadeIn(750);
                }
            } else {
                $("#fh_frame_veil").fadeOut(750);
                $("#freelance_hub_frame iframe").slideUp(500);
                setTimeout("$('#freelance_hub_frame').remove()", 750);
            }
            $("#fh_frame_veil").click(function (event) {
                $("#fh_frame_veil").fadeOut(750);
                $("#freelance_hub_frame iframe").slideUp(500);
                setTimeout("$('#freelance_hub_frame').remove()", 750);
            });
        })();
    }

})();
