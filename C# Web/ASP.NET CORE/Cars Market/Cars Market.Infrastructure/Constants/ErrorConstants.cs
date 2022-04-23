using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars_Market.Infrastructure.Constants
{
    public static class ErrorConstants
    {
        // CREATE SELLER ERROR MESSAGES
        public const string ERROR_TITLE_WHILE_CREATE_SELLER = "Error occured while trying to register this user as a seller";
        public const string ERROR_MESSAGE_WHILE_CREATE_SELLER = "Possible causes user is already registered as a seller, if is not please contact with admin@carsmarket.com";

        // PROFILE WHO DOESNT EXISTS ERROR MESSAGES
        public const string ERROR_TITLE_PROFILE_WHO_DOESNT_EXISTS = "Error occured while looking user profile";
        public const string ERROR_MESSAGE_PROFILE_WHO_DOESNT_EXISTS = "The resource you are looking for (or one of its dependencies) could have been removed," +
                    " had its name changed, or is temporarily unavailable." +
                    " Please review the following URL and make sure that it is spelled correctly.";

        // MESSAGE(S) WHO DOESNT EXISTS ERROR MESSAGES
        public const string DELETE_MESSAGE_ERROR_TITLE = "An error ocurred while trying to delete message";
        public const string READ_MESSAGE_ERROR_TITLE = "An error ocurred while trying to read message";
        public const string DELETE_OR_READ_MESSAGE_ERROR_MESSAGE = "Message doesnt exists / or it was removed";

        // DETAILS WHO DOESNT EXISTS ERROR MESSAGES
        public const string SHOW_DETAILS_ERROR_TITLE = "An error ocurred while trying to see car details";
        public const string SHOW_DETAILS_ERROR_MESSAGE = "The resource you are looking for (or one of its dependencies) could have been removed," +
                " had its name changed, or is temporarily unavailable." +
                " Please review the following URL and make sure that it is spelled correctly.";

        // COMMENT ON CAR ERROR MESSAGES
        public const string COMMENT_CAR_WITH_NULL_ERROR_TITLE = "An error ocurred while trying to post comment to car";
        public const string COMMENT_CAR_WITH_NULL_ERROR_MESSAGE = "You are trying to send empty message!";

        // EDIT CAR ERROR MESSAGES
        public const string EDIT_CAR_ERROR_TITLE = "An error ocurred while trying to edit car information";
        public const string EDIT_CAR_OWNER_ERROR_MESSAGE = "You must be owner of the car in order to edit hers information!";

        public const string EDIT_FORM_REQUIRED_FIELDS_ERROR_MESSAGE = "All fields are required!";

        // EDIT CAR ERROR MESSAGES
        public const string DELETE_CAR_ERROR_TITLE = "An error ocurred while trying to delete car";
        public const string DELETE_CAR_ERROR_MESSAGE = "You must be owner of the car in order to delete it!";
    }

}
