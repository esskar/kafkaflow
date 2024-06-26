using System;
using Confluent.Kafka;
using KafkaFlow.Configuration;
using SaslMechanism = KafkaFlow.Configuration.SaslMechanism;
using SaslOauthbearerMethod = KafkaFlow.Configuration.SaslOauthbearerMethod;
using SecurityProtocol = KafkaFlow.Configuration.SecurityProtocol;
using SslEndpointIdentificationAlgorithm = KafkaFlow.Configuration.SslEndpointIdentificationAlgorithm;

namespace KafkaFlow;

internal static class ConfigurationExtensions
{
    public static Confluent.Kafka.SaslMechanism ToConfluent(this SaslMechanism mechanism) => mechanism switch
    {
        SaslMechanism.Gssapi => Confluent.Kafka.SaslMechanism.Gssapi,
        SaslMechanism.Plain => Confluent.Kafka.SaslMechanism.Plain,
        SaslMechanism.ScramSha256 => Confluent.Kafka.SaslMechanism.ScramSha256,
        SaslMechanism.ScramSha512 => Confluent.Kafka.SaslMechanism.ScramSha512,
        SaslMechanism.OAuthBearer => Confluent.Kafka.SaslMechanism.OAuthBearer,
        _ => throw new ArgumentOutOfRangeException()
    };

    public static Confluent.Kafka.SaslOauthbearerMethod ToConfluent(this SaslOauthbearerMethod method) => method switch
    {
        SaslOauthbearerMethod.Default => Confluent.Kafka.SaslOauthbearerMethod.Default,
        SaslOauthbearerMethod.Oidc => Confluent.Kafka.SaslOauthbearerMethod.Oidc,
        _ => throw new ArgumentOutOfRangeException()
    };

    public static Confluent.Kafka.SecurityProtocol ToConfluent(this SecurityProtocol protocol) => protocol switch
    {
        SecurityProtocol.Plaintext => Confluent.Kafka.SecurityProtocol.Plaintext,
        SecurityProtocol.Ssl => Confluent.Kafka.SecurityProtocol.Ssl,
        SecurityProtocol.SaslPlaintext => Confluent.Kafka.SecurityProtocol.SaslPlaintext,
        SecurityProtocol.SaslSsl => Confluent.Kafka.SecurityProtocol.SaslSsl,
        _ => throw new ArgumentOutOfRangeException()
    };

    public static Confluent.Kafka.SslEndpointIdentificationAlgorithm ToConfluent(this SslEndpointIdentificationAlgorithm algorithm) =>
        algorithm switch
        {
            SslEndpointIdentificationAlgorithm.Https => Confluent.Kafka.SslEndpointIdentificationAlgorithm.Https,
            SslEndpointIdentificationAlgorithm.None => Confluent.Kafka.SslEndpointIdentificationAlgorithm.None,
            _ => throw new ArgumentOutOfRangeException()
        };

    public static void ReadSecurityInformationFrom(this ClientConfig config, ClusterConfiguration cluster)
    {
        var securityInformation = cluster.GetSecurityInformation();

        if (securityInformation is null)
        {
            return;
        }

        config.SecurityProtocol = securityInformation.SecurityProtocol?.ToConfluent();
        config.SslCaLocation = securityInformation.SslCaLocation;
        config.SslCaPem = securityInformation.SslCaPem;
        config.SslCertificateLocation = securityInformation.SslCertificateLocation;
        config.SslCertificatePem = securityInformation.SslCertificatePem;
        config.SslCipherSuites = securityInformation.SslCipherSuites;
        config.SslCrlLocation = securityInformation.SslCrlLocation;
        config.SslCurvesList = securityInformation.SslCurvesList;
        config.SslKeyLocation = securityInformation.SslKeyLocation;
        config.SslKeyPassword = securityInformation.SslKeyPassword;
        config.SslKeyPem = securityInformation.SslKeyPem;
        config.SslKeystoreLocation = securityInformation.SslKeystoreLocation;
        config.SslKeystorePassword = securityInformation.SslKeystorePassword;
        config.SslSigalgsList = securityInformation.SslSigalgsList;
        config.SslEndpointIdentificationAlgorithm = securityInformation.SslEndpointIdentificationAlgorithm?.ToConfluent();
        config.EnableSslCertificateVerification = securityInformation.EnableSslCertificateVerification;
        config.SaslKerberosKeytab = securityInformation.SaslKerberosKeytab;
        config.SaslKerberosPrincipal = securityInformation.SaslKerberosPrincipal;
        config.SaslOauthbearerConfig = securityInformation.SaslOauthbearerConfig;
        config.SaslKerberosKinitCmd = securityInformation.SaslKerberosKinitCmd;
        config.SaslKerberosServiceName = securityInformation.SaslKerberosServiceName;
        config.SaslKerberosMinTimeBeforeRelogin = securityInformation.SaslKerberosMinTimeBeforeRelogin;
        config.EnableSaslOauthbearerUnsecureJwt = securityInformation.EnableSaslOauthbearerUnsecureJwt;
        config.SaslMechanism = securityInformation.SaslMechanism?.ToConfluent();
        config.SaslUsername = securityInformation.SaslUsername;
        config.SaslPassword = securityInformation.SaslPassword;
        config.SaslOauthbearerMethod = securityInformation.SaslOauthbearerMethod?.ToConfluent();
        config.SaslOauthbearerClientId = securityInformation.SaslOauthbearerClientId;
        config.SaslOauthbearerClientSecret = securityInformation.SaslOauthbearerClientSecret;
        config.SaslOauthbearerTokenEndpointUrl = securityInformation.SaslOauthbearerTokenEndpointUrl;
        config.SaslOauthbearerScope = securityInformation.SaslOauthbearerScope;
    }
}