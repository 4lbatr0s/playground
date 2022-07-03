public class Student extends User {
    private String[] commments;

    public String[] getCommments () {
        return commments;
    }

    public void setCommments ( String[] commments ) {
        this.commments = commments;
    }

    public boolean isKickedFromServer () {
        return kickedFromServer;
    }

    public void setKickedFromServer ( boolean kickedFromServer ) {
        this.kickedFromServer = kickedFromServer;
    }

    private boolean kickedFromServer;



}




