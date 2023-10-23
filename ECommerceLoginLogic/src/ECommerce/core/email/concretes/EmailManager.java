package ECommerce.core.email.concretes;

import ECommerce.core.email.abstracts.IEmailService;

public class EmailManager implements IEmailService {
    @Override
    public void SendEmail ( String email ) {
        System.out.println(email);
    }
}
