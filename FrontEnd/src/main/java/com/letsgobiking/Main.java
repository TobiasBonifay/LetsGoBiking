package com.letsgobiking;

import java.util.Scanner;

import static com.letsgobiking.MainLogic.*;

public class Main {
    private static final Scanner sc = new Scanner(System.in);

    public static void main(String[] args) {
        System.out.println("Hello !");
        try {
            showHelp();
        } catch (ExceptionInInitializerError e) {
            System.err.println("Server is not available");
            e.printStackTrace();
            System.exit(1);
        }

        while (true) {
            System.out.println("\n\nEnter a command: ");
            String inputCleared = sc.nextLine().trim().substring(0, 1).toLowerCase();
            switch (inputCleared) {
                case "p", "q", "quit" -> {
                    return;
                }
                case "4" -> getWayInstructionsMq();
                case "0" -> getWayInstructions();
                case "1" -> findClosestStation();
                case "2" -> getGPSCoordsFromAddress();
                case "3" -> getStations();
                default -> showHelp();
            }
        }
    }
}
