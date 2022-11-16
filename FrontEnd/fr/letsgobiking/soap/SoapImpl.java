package fr.letsgobiking.soap;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.Scanner;
import java.util.stream.Collectors;

public class SoapImpl implements ISoap {

    private final String urlString;
    private final String namespaceUri;
    private final String wsOperation;
    private HttpURLConnection connection;

    public SoapImpl() {
        this.urlString = "http://localhost:8080/soap";
        this.namespaceUri = "http://soap.letsgobiking.fr/";
        this.wsOperation = "sendSoapRequest";
    }

    public String sendSoapRequest(String startAddress, String endAddress) {
        try {
            openConnection();

            final int responseCode = connection.getResponseCode();
            final String responseMessage = connection.getResponseMessage();
            if (responseMessage != null && responseCode == 200) {
                final InputStream inputStream = connection.getInputStream();
                final String response = new BufferedReader(new InputStreamReader(inputStream)).lines().collect(Collectors.joining("\n"));
                System.out.println(response);
                return response;
            } else {
                createErrorMessage(responseCode, responseMessage);
            }
        } catch (IOException e) {
            throw new RuntimeException("Couldn't send SOAP request", e);
        } finally {
            closeConnection();
        }
        return null;
    }

    /**
     * Handle the error message
     *
     * @param responseCode    HTTP response code
     * @param responseMessage HTTP response message
     * @throws IOException if the connection can't be opened
     */
    private void createErrorMessage(int responseCode, String responseMessage) {
        InputStream errorStream = connection.getErrorStream();
        Scanner errorStreamScanner = new Scanner(errorStream).useDelimiter("\\A");
        String errorString = String.format("HTTP code %d %nHTTP response was \"%s\"", responseCode, responseMessage);
        if (errorStreamScanner.hasNext()) {
            errorString += String.format(" with error message \"%s\"", errorStreamScanner.next());
        }
        throw new RuntimeException(errorString);
    }

    /**
     * Initialize the connection to the SOAP server
     *
     * @throws IOException if the connection can't be opened
     */
    @Override
    public void openConnection() throws IOException {
        URL url = new URL(String.format("%s.asmx?op=%s", urlString, wsOperation));
        connection = (HttpURLConnection) url.openConnection();
        connection.setRequestMethod("POST");
        connection.setRequestProperty("Content-Type", "text/xml; charset=utf-8");
        connection.setRequestProperty("SOAPAction", String.format("%s/%s", namespaceUri, wsOperation));
        connection.setDoOutput(true);
    }

    @Override
    public void closeConnection() {
        if (connection != null) {
            connection.disconnect();
        }
    }
}

