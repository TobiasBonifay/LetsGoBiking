package fr.letsgobiking.soap;

import java.io.IOException;

public interface ISoap {
    String sendSoapRequest(String start, String end);

    void openConnection() throws IOException;

    void closeConnection();

}