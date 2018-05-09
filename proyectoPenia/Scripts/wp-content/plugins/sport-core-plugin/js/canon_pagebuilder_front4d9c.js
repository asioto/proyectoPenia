"use strict";

/*************************************************************
PAGEBUILDER FRONT SCRIPTS

ADD PB_FIRST PB_LAST CLASSES TO PB BLOCKS
GALLERY BLOCK ISOTOPE FILTERING
POSTS GRAPH

*************************************************************/

/*****************************************
ADD PB_FIRST PB_LAST CLASSES TO PB BLOCKS
*****************************************/


	jQuery(document).ready(function($) {

		if ($('.pb_block').size() > 0) {

			var $pbBlocks = $('.pb_block');
			$pbBlocks.first().addClass('pb_block_first');
			$pbBlocks.last().addClass('pb_block_last');

		}
		
	});

/*****************************************
GALLERY BLOCK ISOTOPE FILTERING
*****************************************/


	jQuery(window).load(function($) {

		$=jQuery;

		if ($('.pb_isotope_gallery').size() > 0) {
			$('.pb_isotope_gallery').isotope({
				itemSelector: '.element',
				layoutMode: 'fitRows'
			});
		}

		// $('.pb_isotope_gallery').isotope('reLayout');

	});

	jQuery(document).ready(function($) {

		
		if ($('.isotope_filter_menu').size() > 0) {


			
			//ISOTOPE FILTERING
			$('.isotope_filter_menu li:eq(0) a').addClass('selected');

			$('.isotope_filter_menu li a').on('click', function (event) {
				event.preventDefault();
				var $this = $(this);
				var $this_block = $this.closest('.pb_block');
				var $this_gallery = $this_block.find('.pb_isotope_gallery');

				//update selected filter item
				$('.isotope_filter_menu li a').removeClass('selected');
				$this.addClass('selected');


				var filterVar = $this.closest('li').attr('class');
				if ( (typeof filterVar == 'undefined') || (filterVar.indexOf('cat-item-all') != -1) )  {
					filterVar = "*";
				} else {
					filterVar = filterVar.split(' ');
					filterVar = "." + filterVar[1];
				}
				$this_gallery.isotope({ filter: filterVar});

				//recalculate last item
				var $filteredItems = $this_gallery.find('.element:not(.isotope-hidden)');
				if ($filteredItems.size() > 0) {
					var	numColumns = $this_gallery.attr('data-num_columns');
 
					$filteredItems.each(function(index, e) {
						var $this = $(this);
						$this.removeClass('last');
						if (((index+1) % numColumns) === 0) $this.addClass('last');
					});

				$this_gallery.isotope('reLayout');
						
				}
			});

		}
		
	});

/*****************************************
POSTS GRAPH
*****************************************/


	jQuery(document).ready(function($) {

		if ($('.pb_posts_graph').size() > 0) {

			$('.pb_posts_graph'). each(function (index) {
				var $this = $(this);
				var $singleGraphs = $this.find('.single-graph');
				var containerHeight = 260;
				var imageHeight = 84;
				var pixelRange = containerHeight - imageHeight;

				// determine max
				var max = 0;
				$singleGraphs.each(function (index) {
					var $thisSingleGraph = $(this);
					var yValue = parseInt($thisSingleGraph.find('.graph-inner').attr('data-y_value'));
					if (yValue > max) { max = yValue; }
				});

				// for each graph calculate percentage of max
				$singleGraphs.each(function (index) {
					var $thisSingleGraph = $(this);
					var $thisInnerGraph = $thisSingleGraph.find('.graph-inner');
					var yValue = parseInt($thisInnerGraph.attr('data-y_value'));
					var percentage = yValue / max;
					var reversePercentage = 1-percentage;
					var pixelsFromTop = pixelRange * reversePercentage;
					$thisInnerGraph.css('top', pixelsFromTop + 'px');

					// console.log("yValue: " + yValue);
					// console.log("percentage: " + percentage);
					// console.log("reversePercentage: " + reversePercentage);
					// console.log("pixelsFromTop: " + pixelsFromTop);
				});

			});

		}
		
	});

