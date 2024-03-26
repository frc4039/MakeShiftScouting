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

	$('#scoutingForm').on('submit', function (e) {
		output = getSanitizedOutput();

		$('#savedData').val(output);

		//generate QR Code from data
		const qrCodeDiv = $('#qrcode')[0];
		qrCodeDiv.innerHTML = "";
		new QRCode(qrCodeDiv, output);

		//Open popup with QR Code
		$('#qrCodeModal').modal('show');
		$('#modalTitle').text($('#robot').val() + " - " + $('#teamNumber').val());
		e.preventDefault();
	});

	$('#resetFields').on('click', function () {
		output = getSanitizedOutput();

		$('#savedData').val(output);

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

	$('#recoverFields').on('click', function () {
		const form = $('#scoutingForm')[0];
		if ($('#savedData').val() != '') {
			const reload = $('#savedData').val().split('\t');
			const submitter = $('#generateQrCode')[0];
			const formData = new FormData(form, submitter);
			iCount = 0;
			for (const [key, value] of formData) {
				if (reload[iCount] == 'true') {
					$('[data-id=' + key + ']').prop("checked", true);
				}
				else {
					$('#' + key).val(reload[iCount]);
				}
				iCount++;
			}
		}
		$('#savedData').val('');
	});

	function getSanitizedOutput() {
		//gather the form data into a tab separated string
		const form = $('#scoutingForm')[0];
		const submitter = $('#generateQrCode')[0];
		const formData = new FormData(form, submitter);
		let output = "";
		for (const [key, value] of formData) {
			output += `${value}\t`;
		}

		//sanitize the output for CR's
		output = output.replace(/(\r\n|\n|\r)/gm, " - ");
		return output;
	};
});