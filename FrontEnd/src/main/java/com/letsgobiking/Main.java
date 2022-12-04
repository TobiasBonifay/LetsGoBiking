package com.letsgobiking;

import com.letsgobiking.generated.IRoutingService;
import com.letsgobiking.generated.RoutingService;

import java.util.Scanner;

public class Main {

    private static final Scanner sc = new Scanner(System.in);

    public static void main(String[] args) {

        RoutingService routingService = new RoutingService();
        IRoutingService iRoutingService = routingService.getBasicHttpBindingIRoutingService();

        System.out.println("Hello !");
        while (true) {
            showHelp();
            System.out.println("Enter a command: ");

            switch (sc.nextLine().trim().substring(0, 1).toLowerCase()) {
                case "p", "q", "quit" -> {
                    return;
                }
                case "h" -> showHelp();
                case "0" -> {
                    System.out.println("Get best itinerary instructions");
                    System.out.println("Enter a departure address: ");
                    String fromCoords = sc.nextLine();
                    System.out.println("Enter a destination address: ");
                    String toCoords = sc.nextLine();
                    System.out.println(iRoutingService.getWayInstructions(fromCoords, toCoords));
                }
                case "1" -> {
                    System.out.println("Find the closest station");
                    System.out.println(iRoutingService.findClosestStation());
                }
                case "2" -> {
                    System.out.println("Get GPS coordinates from address");
                    System.out.println("Enter an address: ");
                    String address = sc.nextLine();
                    System.out.println(iRoutingService.getGPSCoordsFromAddress(address));
                }
                case "3" -> {
                    System.out.println("Get available stations");
                    System.out.println(iRoutingService.getStations());
                }
                default -> {
                    System.out.println("Unknown command");
                    showHelp();
                }
            }
        }
    }

    private static void showHelp() {
        System.out.println("------------------------");
        System.out.println("0: Get best itinerary instructions");
        System.out.println("1: Find the closest station");
        System.out.println("2: Get GPS coordinates from address");
        System.out.println("3: Get available stations");
        System.out.println("h: Show help");
        System.out.println("q: Quit");
        System.out.println("------------------------");
    }
}
