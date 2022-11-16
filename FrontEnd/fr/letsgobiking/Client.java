package fr.letsgobiking;

import fr.letsgobiking.soap.ISoap;
import fr.letsgobiking.soap.SoapImpl;

import java.io.IOException;
import java.util.Scanner;

/**
 * Java client (SOAP)
 */
public class Client {
    private static final Scanner sc = new Scanner(System.in);
    private final ISoap soap;

    public Client() {
        soap = new SoapImpl();
    }

    // Start the client
    public void start() throws IOException {
        System.out.println("Hello !");
        System.out.println("Please enter the starting point of your trip :");
        String start = sc.nextLine();
        System.out.println("Please enter the destination of your trip :");
        String end = sc.nextLine();

        soap.openConnection();
        soap.sendSoapRequest(start, end);
        soap.closeConnection();
    }
}