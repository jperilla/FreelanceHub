/*
 * SimpleModal OSX Style Modal Dialog
 * http://www.ericmmartin.com/projects/simplemodal/
 * http://code.google.com/p/simplemodal/
 *
 * Copyright (c) 2010 Eric Martin - http://ericmmartin.com
 *
 * Licensed under the MIT license:
 *   http://www.opensource.org/licenses/mit-license.php
 *
 * Revision: $Id: osx.js 238 2010-03-11 05:56:57Z emartin24 $
 */

jQuery(function ($) {

    function IsJsonString(str) {
        try {
            JSON.parse(str);
        } catch (e) {
            return false;
        }
        return true;
    }

    var OSX = {
        container: null,
        init: function () {
            var openModal = function () {
                e.preventDefault();

                $('#plan').val(this.id);

                $("#osx-modal-content").modal({
                    overlayId: 'osx-overlay',
                    containerId: 'osx-container',
                    closeHTML: null,
                    minHeight: 80,
                    opacity: 65,
                    position: ['0', ],
                    overlayClose: true,
                    onOpen: OSX.open,
                    onClose: OSX.close
                });
            };

            $("input.osx, a.osx, button.osx").click(function (e) {
                e.preventDefault();

                $('#plan').val(this.id);

                $("#osx-modal-content").modal({
                    overlayId: 'osx-overlay',
                    containerId: 'osx-container',
                    closeHTML: null,
                    minHeight: 80,
                    opacity: 65,
                    position: ['0', ],
                    overlayClose: true,
                    onOpen: OSX.open,
                    onClose: OSX.close
                });
            });
        },
        open: function (d) {
            var self = this;
            self.container = d.container[0];
            d.overlay.fadeIn('slow', function () {
                $("#osx-modal-content", self.container).show();
                var title = $("#osx-modal-title", self.container);
                title.show();
                d.container.slideDown('slow', function () {
                    setTimeout(function () {
                        var h = $("#osx-modal-data", self.container).height() + 40; // padding
                        d.container.animate(
							{ height: h },
							200,
							function () {
							    $("div.close", self.container).show();
							    $("#osx-modal-data", self.container).show();
							}
						);
                    }, 300);
                });
            })
        },
        close: function (d) {
            var self = this; // this = SimpleModal object
            d.container.animate(
				{ top: "-" + (d.container.height() + 20) },
				500,
				function () {
				    self.close(); // or $.modal.close();
				}
			);
        }
    };

    OSX.init();

    var OSXLogin = {
        container: null,
        init: function () {
            $("input.osx-login, a.osx-login, button.osx-login").click(function (e) {
                e.preventDefault();

                $("#osx-modal-content-login").modal({
                    overlayId: 'osx-overlay',
                    containerId: 'osx-container',
                    closeHTML: null,
                    minHeight: 80,
                    opacity: 65,
                    position: ['0', ],
                    overlayClose: true,
                    onOpen: OSXLogin.open,
                    onClose: OSXLogin.close
                });
            });
        },
        open: function (d) {
            var self = this;
            self.container = d.container[0];
            d.overlay.fadeIn('slow', function () {
                $("#osx-modal-content-login", self.container).show();
                var title = $("#osx-modal-title-login", self.container);
                title.show();
                d.container.slideDown('slow', function () {
                    setTimeout(function () {
                        var h = $("#osx-modal-data-login", self.container).height() + 40; // padding
                        d.container.animate(
							    { height: h },
							    200,
							    function () {
							        $("div.close", self.container).show();
							        $("#osx-modal-data-login", self.container).show();
							    }
						    );
                    }, 300);
                });
            })
        },
        close: function (d) {
            var self = this; // this = SimpleModal object
            d.container.animate(
				    { top: "-" + (d.container.height() + 20) },
				    500,
				    function () {
				        self.close(); // or $.modal.close();
				    }
			    );
        }
    };

    OSXLogin.init();

});