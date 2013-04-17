var serviceUrl = '/Service/Mongo/SecretCommunicatorService.svc';

/*
    Registers a new channel through post request.
*/
function registerChannel(channelName, channelPassword, responseAlert) {
    $.ajax({
        type: 'post',
        url: serviceUrl + '/register?name=' + channelName + '&pwd=' + channelPassword,
        success: function () {
            displaySuccess(responseAlert, "Channel registered successfully");
        },
        error: function (jxh, text, errorThrown) {
            displayError(responseAlert, errorThrown);
        }
    });
}

function displayError(alert, error) {
    alert.removeClass('alert-success');
    alert.addClass('alert-error');
    alert.find('.alert-heading').text(error);
    alert.show();
}

function displaySuccess(alert, message){
    alert.removeClass('alert-error');
    alert.addClass('alert-success');
    alert.find('.alert-heading').text(message);
    alert.show();
}