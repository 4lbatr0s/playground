package Utils;

import java.util.UUID;

public class GuidOperations {
    public static String CreateRandomGuid(){
        String uuid = UUID.randomUUID().toString();
        return uuid;
    }
}
