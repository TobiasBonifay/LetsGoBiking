package com.letsgobiking;

import org.junit.Test;

import java.io.ByteArrayInputStream;
import java.io.InputStream;

public class Tests {
    public static void main(String[] args) {
        System.out.println("Hello !");
        nantes();
    }

    @Test
    public static void nantes() {
        String input = """
                23 avenue Chanzy nantes
                12 mail pablo picasso nantes
                """;
        InputStream inputStream = new ByteArrayInputStream(input.getBytes());

        System.setIn(inputStream);

        MainLogic.getWayInstructions();
    }
}