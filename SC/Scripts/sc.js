var serviceUrl = 'Service/Mongo/SecretCommunicatorService.svc';

/*
    Registers a new channel through post request.
*/
function registerChannel(channelName, channelPassword, responseAlert) {
    $.ajax({
        type: 'post',
        url: serviceUrl + '/register',
        data: JSON.stringify({ Name: channelName, Password: channelPassword }),
        contentType: 'application/json',
        success: function () {
            displaySuccess(responseAlert, "Channel registered successfully");
        },
        error: function (jxh, text, errorThrown) {
            displayError(responseAlert, errorThrown);
        }
    });
}

/*
    Creates a secret through post request.
*/
function createMessage(channelName, channelPassword, text, responseAlert) {
    $.ajax({
        type: 'post',
        url: serviceUrl + '/message/create',
        data: JSON.stringify({ channel: {Name: channelName, Password: channelPassword}, text: text }),
        contentType: 'application/json',
        success: function () {
            displaySuccess(responseAlert, "Message created");
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