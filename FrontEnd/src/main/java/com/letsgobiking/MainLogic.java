package com.letsgobiking;

import com.letsgobiking.generated.IRoutingService;
import com.letsgobiking.generated.RoutingService;

import java.util.Scanner;

public class MainLogic {
    private static final Scanner sc = new Scanner(System.in);
    private static final RoutingService routingService = new RoutingService();
    private static final IRoutingService iRoutingService = routingService.getBasicHttpBindingIRoutingService();

    private MainLogic() {
        // private constructor to prevent instantiation -> static class -> constructor hidden
    }

    static void getWayInstructions() {
        System.out.println("Get best itinerary instructions");
        System.out.println("Enter a departure address: ");
        String fromCoords = sc.nextLine();
        String startClosestStation = iRoutingService.findClosestStation(fromCoords);
        System.out.println("The nearest station is " + startClosestStation);
        System.out.println("Enter a destination address: ");
        String toCoords = sc.nextLine();
        String endClosestStation = iRoutingService.findClosestStation(toCoords);
        System.out.println(iRoutingService.getWayInstructions(fromCoords, startClosestStation, toCoords, endClosestStation));
    }

    static void findClosestStation() {
        System.out.println("Find the closest station");
        System.out.println("Enter an address: ");
        String address = sc.nextLine();
        System.out.println(iRoutingService.findClosestStation(address));
    }

    static void getGPSCoordsFromAddress() {
        System.out.println("Get GPS coordinates from address");
        System.out.println("Enter an address: ");
        String address = sc.nextLine();
        System.out.println(iRoutingService.getGPSCoordsFromAddress(address));
    }

    static void getStations() {
        System.out.println("Get available stations");
        System.out.println(iRoutingService.getStations());
    }

    static void showHelp() {
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
