$(document).ready(function() {
		$('.minus').click(function () {
			var $input = $(this).parent().find('input');
			if (isNaN($input.val())) { $input.val(0); }
			var count = parseInt($input.val()) - 1;
			count = count < 0 ? 0 : count;
			$input.val(count);
			$input.change();
			return false;
		});
		$('.plus').click(function () {
			var $input = $(this).parent().find('input');
			if (isNaN($input.val())) { $input.val(0); }
			$input.val(parseInt($input.val()) + 1);
			$input.change();
			return false;
		});

	$('#generateQrCode').on('submit', function (e) {
		$('#qrCodeModal').modal('show');
		$('#modalTitle').text($('#robot').val() + " - " + $('#teamNumber').val());
		e.preventDefault();
	});

	$('#resetFields').on('click', function () {
		$.each($('.resettingList'), function () { $(this).get(0).selectedIndex = 0; });
		$.each($('.resettingText'), function () { $(this).val(''); });
		$.each($('.plainNumber'), function () { $(this).val(''); });
		$.each($('.checkboxStyle'), function () { $(this).prop("checked", false); });
		$.each($('.counter'), function () { $(this).val(0); });
		$.each($('.autoNumber'), function () {
			if (isNaN($(this).val()) || $(this).val() == '') { $(this).val(0); }
			$(this).val(parseInt($(this).val()) + 1);
		});
	});
});