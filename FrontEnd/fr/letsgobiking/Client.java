package fr.letsgobiking;

import fr.letsgobiking.soap.ISoap;
import fr.letsgobiking.soap.SoapImpl;

import java.net.HttpURLConnection;
import java.util.Scanner;

/**
 * Java client (SOAP)
 */
public class Client {
    private static final Scanner sc = new Scanner(System.in);
    private static final ISoap soap = new SoapImpl();
    private final String urlString;
    private final String namespaceUri;
    private final String wsOperation;
    private HttpURLConnection connection;

    public Client() {
        // TODO: Set the URL of the web service
        this.urlString = "http://localhost:8080/soap";
        this.namespaceUri = "http://soap.letsgobiking.fr/";
        this.wsOperation = "sendSoapRequest";
    }

    // Start the client
    public void start() {
        System.out.println("Hello !");
        System.out.println("Please enter the starting point of your trip :");
        String start = sc.nextLine();
        System.out.println("Please enter the destination of your trip :");
        String end = sc.nextLine();

        soap.sendSoapRequest(start, end);
    }
}