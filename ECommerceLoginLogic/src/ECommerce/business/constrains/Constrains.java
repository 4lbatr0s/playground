package ECommerce.business.constrains;

public class Constrains {
    public static  String getRegisteredEmail () {
        return registeredEmail;
    }

    public static void setRegisteredEmail ( String registeredEmail ) {
        Constrains.registeredEmail = registeredEmail;
    }

    private static String registeredEmail = "Click the link to activate your account";
    private static String activationSuccesful = "Account successfully activated";
    private static String activationFailed = "Account failed to activate";
    private static String clickedActivationLink = "Activation link clicked";

    public static String getActivationSuccesful () {
        return activationSuccesful;
    }

    public static void setActivationSuccesful ( String activationSuccesful ) {
        Constrains.activationSuccesful = activationSuccesful;
    }

    public static String getActivationFailed () {
        return activationFailed;
    }

    public static void setActivationFailed ( String activationFailed ) {
        Constrains.activationFailed = activationFailed;
    }

    public static String getClickedActivationLink () {
        return clickedActivationLink;
    }

    public static void setClickedActivationLink ( String clickedActivationLink ) {
        Constrains.clickedActivationLink = clickedActivationLink;
    }
}
