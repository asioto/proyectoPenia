"use strict";

/*************************************************************
WIDGETS PLUGIN SCRIPTS

TWITTER FEED WITH THEME DESIGN
WIDGET: FACT BOX FITTEXT
WIDGET: TABS
TOGGLE
ANIMATED NUMBER
DONUT CHART

*************************************************************/


/*************************************************************
TWITTER FEED WITH THEME DESIGN
*************************************************************/

	jQuery(document).ready(function($) {
		if ($('.twitter_theme_design').size() > 0) {

			var $twitterThemeDesignContainer = $('.twitter_theme_design');

			$twitterThemeDesignContainer.each(function(index) {

				var $this = $(this);

				var useThemeDesign = $this.attr('data-theme_design');
				if (useThemeDesign == "false") {
					$this.hide();
				} else {
					var $associatedTwitterWidget = $this.prev('.twitter_widget');
					$associatedTwitterWidget.hide();

					$(window).load(function() {
						//set vars
						var success = false;
						var delay = 100;
						var attempts = 10;

						for (var $i = 1; $i < attempts+1; $i++) {  
									
								setTimeout(function() {
									if (success === false) {
										var $twitterIframe = $this.prev('.twitter_widget').find('iframe');
									    if ($twitterIframe.contents().find('.timeline-Tweet').size() > 0) {
									    	success = true;	

											//get and post tweets
											var numTweets = $this.attr('data-num_tweets');
											var tweetCount = 0;
											$twitterIframe.contents().find('.timeline-Tweet').each(function(index, e){
												if (tweetCount == numTweets) return;
												var $this = $(this);
												var published = $this.find('time').text();
												var tweet = $this.find('.timeline-Tweet-text').html();
												var altTweet = "<li class='tweet'><p>" + tweet + "</p><h6 class='meta'>" + published + "</span></li>";
												var $associatedTwitterThemeDesignContainer = $twitterIframe.closest('.twitter_widget').next('.twitter_theme_design');
												$associatedTwitterThemeDesignContainer.find('ul').append(altTweet);
												tweetCount++;
											});
									    }
									}

								}, delay*$i);

						} // end fori

					}); // end on window load

				} // end if else

			}); // end each instance

		}

	});

/*************************************************************
WIDGET: FACT BOX FITTEXT
*************************************************************/

	jQuery(document).ready(function($) {
		
		if ($('.fittext').size() > 0) {
			if (typeof fitText == "function") {
				$('.fittext').each(function(index, el) {
					var $this = $(this);
					var ratio = $this.attr('data-ratio');
					fitText($this, ratio);
				});
			}
		}

	});


/*************************************************************
WIDGET: TABS
*************************************************************/

	jQuery(document).ready(function($) {
		
		if ($('.widget.sport_tabs').size() > 0) {

			$('.widget.sport_tabs').each(function(index) {
				var $this = $(this);
				var widgetID = $this.attr('id');
				$('#'+ widgetID + '-tab-container').cleanTabs();
			});
		}

	});



/*************************************************************
TOGGLE
*************************************************************/

	jQuery(document).ready(function($){

		if ($('.toggle-btn').size() > 0) {

			// toggle	  
			$('.toggle-btn').click(function(e){
				e.preventDefault();
				$(this).closest('li').find('.toggle-content').not(':animated').slideToggle();
				$(this).toggleClass("active");
			});
		}

	});



/*************************************************************
ANIMATED NUMBER
*************************************************************/

	jQuery(document).ready(function($){

		if ($('.canon_animated_number').size() > 0) {

			$('.canon_animated_number').each(function(index) {
				var $this = $(this);
				var number = parseInt($this.attr('data-number'));
				var useSeperator = $this.attr('data-seperator');
				var useSeperatorBoolean = (useSeperator == 'checked') ? true : false;
				var animationSpeed = parseInt($this.attr('data-animation_speed'));

				var $thisAnimatedNumberWrapper = $this.find('.canon_animated_number_wrapper');
				$thisAnimatedNumberWrapper.animateNumbers(number, useSeperatorBoolean, animationSpeed);
			});

		}

	});


/*************************************************************
DONUT CHART
*************************************************************/

	jQuery(document).ready(function($){


		$('.donut_chart').each(function(index) {
			var $thisChart = $(this);
			var thisChartID = $thisChart.attr('id');
			var instance = $.parseJSON($thisChart.attr('data-instance'));
			var chartData = instance.chart_data;
			var postfix = instance.postfix;
			var colorArray = [];

			$.each(chartData, function (index, value) {
				colorArray.push(value.color);
			});

			Morris.Donut({
				element: thisChartID,
				data: chartData,
				colors: colorArray,
				formatter: function (y, data) { return y + postfix }, 
			}).select(0);

		});

	});
