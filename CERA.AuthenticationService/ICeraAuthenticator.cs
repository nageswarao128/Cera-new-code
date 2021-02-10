namespace CERA.AuthenticationService
{
    public interface ICeraAuthenticator
    {
        public object GetCertificate();
        public object GetJwtToken();
    }
}
