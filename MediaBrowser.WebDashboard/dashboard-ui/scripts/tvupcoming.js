﻿(function ($, document) {

    $(document).on('pagebeforeshowready', "#tvUpcomingPage", function () {

        var page = this;

        if (LibraryMenu.getTopParentId()) {

            $('.scopedLibraryViewNav', page).show();
            $('.globalNav', page).hide();

        } else {
            $('.scopedLibraryViewNav', page).hide();
            $('.globalNav', page).show();
        }

    }).on('pagebeforeshowready', "#tvUpcomingPage", function () {

        Dashboard.showLoadingMsg();

        var page = this;

        var limit = AppInfo.hasLowImageBandwidth ?
         24 :
         40;

        var query = {

            Limit: limit,
            Fields: "AirTime,UserData,SeriesStudio,SyncInfo",
            UserId: Dashboard.getCurrentUserId(),
            ImageTypeLimit: 1,
            EnableImageTypes: "Primary,Backdrop,Banner,Thumb"
        };

        query.ParentId = LibraryMenu.getTopParentId();

        var context = '';

        if (query.ParentId) {

            context = 'tv';

        }

        ApiClient.getJSON(ApiClient.getUrl("Shows/Upcoming", query)).done(function (result) {

            var items = result.Items;

            if (items.length) {
                page.querySelector('.noItemsMessage').style.display = 'none';
            } else {
                page.querySelector('.noItemsMessage').style.display = 'block';
            }

            var elem = page.querySelector('#upcomingItems');
            elem.innerHTML = LibraryBrowser.getPosterViewHtml({
                items: items,
                showLocationTypeIndicator: false,
                shape: "backdrop",
                showTitle: true,
                showPremiereDate: true,
                showPremiereDateIndex: true,
                preferThumb: true,
                context: context || 'home-upcoming',
                lazy: true,
                showDetailsMenu: true

            });

            ImageLoader.lazyChildren(elem);

            Dashboard.hideLoadingMsg();

        });
    });


})(jQuery, document);