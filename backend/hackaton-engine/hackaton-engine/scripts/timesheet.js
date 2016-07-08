(function() {
  'use strict';

  /**
   * Initialize a Timesheet
   */
  var Timesheet = function(container, min, max, data) {
    this.data = [];
    this.year = {
      min: min,
      max: max
    };

    this.parse(data || []);

    if (typeof document !== 'undefined') {
      this.container = (typeof container === 'string') ? document.querySelector('#'+container) : container;
      this.drawSections();
      this.insertData();
    }
  };

  /**
   * Insert data into Timesheet
   */
  Timesheet.prototype.insertData = function() {
    var html = [];
    var widthDay = this.container.querySelector('.scale section').offsetWidth;

    for (var n = 0, m = this.data.length; n < m; n++) {
      var cur = this.data[n];
      var bubble = this.createBubble(widthDay, this.year.min, cur.start, cur.end);

      var line = [
        '<p style="margin-left: ' + bubble.getStartOffset() + 'px; width: ' + bubble.getWidth() + 'px;" class="bubble bubble-' + (cur.type || 'default') + '" data-duration="' + (cur.end ? Math.round((cur.end-cur.start)/1000/60/60/24/39) : '') + '" title="'+bubble.getDateLabel()+'"></p>',
        '<span class="date">' + bubble.getDateLabel() + '</span>',
        '<span class="label">' + cur.label + '</span>'
      ].join('');

      html.push('<li>' + line + '</li>');
    }

    this.container.innerHTML += '<ul class="data">' + html.join('') + '</ul>';
  };

  /**
   * Draw section labels
   */
  Timesheet.prototype.drawSections = function() {
    var html = [];
	var times = moment.duration(moment(this.year.max).diff(moment(this.year.min))).asDays();
	for(var c=0; c<times;c++) {
      html.push('<section style="width: '+(100/times)+'%;">' + moment(this.year.max).add(c,'days').format('YYYY/MM/DD') + '</section>');
    }

    this.container.className = 'timesheet color-scheme-default';
    this.container.innerHTML = '<div class="scale">' + html.join('') + '</div>';
  };

  /**
   * Parse data string
   */
  Timesheet.prototype.parseDate = function(date) {
    if (date.indexOf('/') === -1) {
      date = new Date(parseInt(date, 10), 0, 1);
      date.hasMonth = false;
    } else {
      date = date.split('/');
      date = new Date(parseInt(date[0], 10), parseInt(date[1], 10)-1, parseInt(date[2], 10));
      date.hasMonth = true;
    }

    return date;
  };

  /**
   * Parse passed data
   */
  Timesheet.prototype.parse = function(data) {
    for (var n = 0, m = data.length; n<m; n++) {
		console.log(data[n])
      var beg = this.parseDate(data[n][0]);
      var end = data[n].length === 4 ? this.parseDate(data[n][1]) : null;
      var lbl = data[n].length === 4 ? data[n][2] : data[n][1];
      var cat = data[n].length === 4 ? data[n][3] : data[n].length === 4 ? data[n][2] : 'default';

      if (beg.getFullYear() < this.year.min) {
        this.year.min = beg.getFullYear();
      }

      if (end && end.getFullYear() > this.year.max) {
        this.year.max = end.getFullYear();
      } else if (beg.getFullYear() > this.year.max) {
        this.year.max = beg.getFullYear();
      }

      this.data.push({start: beg, end: end, label: lbl, type: cat});
    }
  };

  /**
   * Wrapper for adding bubbles
   */
  Timesheet.prototype.createBubble = function(wMonth, min, start, end) {
    return new Bubble(wMonth, min, start, end);
  };

  /**
   * Timesheet Bubble
   */
  var Bubble = function(wMonth, min, start, end) {
    this.min = min;
    this.start = start;
    this.end = end;
    this.widthDay = wMonth;
  };

  /**
   * Format month number
   */
  Bubble.prototype.formatMonth = function(num) {
    num = parseInt(num, 10);

    return num >= 10 ? num : '0' + num;
  };

  /**
   * Calculate starting offset for bubble
   */
  Bubble.prototype.getStartOffset = function() {
	//todo - obliczyc ile dni od poczatku!!!
    return this.widthDay * moment.duration(moment(this.start).diff(moment(this.min))).asDays();
  };

  /**
   * Get count of full years from start to end
   */
  Bubble.prototype.getFullYears = function() {
    return ((this.end && this.end.getFullYear()) || this.start.getFullYear()) - this.start.getFullYear();
  };

  /**
   * Get count of all months in Timesheet Bubble
   */
  Bubble.prototype.getDays = function() {

	return moment.duration(moment(this.end).diff(moment(this.start))).asDays();
  };

  /**
   * Get bubble's width in pixel
   */
  Bubble.prototype.getWidth = function() {
    return (this.widthDay) * this.getDays();
  };

  /**
   * Get the bubble's label
   */
  Bubble.prototype.getDateLabel = function() {
    return moment(this.start).format('YYYY/MM/DD') +' - '+ moment(this.end).format('YYYY/MM/DD');
  };

  window.Timesheet = Timesheet;
})();