package com.letsgobiking;

import com.letsgobiking.generated.IRoutingService;
import com.letsgobiking.generated.RoutingService;

import java.util.Optional;
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
        String fromAddress = sc.nextLine();
        String fromCoords = iRoutingService.getGPSCoordsFromAddress(fromAddress);

        Optional<String> startClosestStation = getClosestStation(fromAddress);
        if (startClosestStation.isEmpty()) return;

        System.out.println("Enter a destination address: ");
        String toAddress = sc.nextLine();
        String toCoords = iRoutingService.getGPSCoordsFromAddress(toAddress);

        Optional<String> endClosestStation = getClosestStation(toAddress);
        if (endClosestStation.isEmpty()) return;

        System.out.println("Waiting for response...");

        String response = iRoutingService.getWayInstructions(fromCoords, startClosestStation.get(), toCoords, endClosestStation.get());
        System.out.println(response);
    }

    static Optional<String> getClosestStation(String address) {
        final String r = iRoutingService.findClosestStation(address);
        if (r.equals("404")) {
            System.err.println("The service is not available in the current city");
            return Optional.empty();
        }
        System.err.println("The closest station is " + r);
        return Optional.of(r);
    }

    static void findClosestStation() {
        System.out.println("Find the closest station");
        System.out.println("Enter an address: ");
        String address = sc.nextLine();
        System.out.println(getClosestStation(address));
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
