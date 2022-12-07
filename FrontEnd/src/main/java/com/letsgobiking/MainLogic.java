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
        Optional<String> response = getWayInstructionsLogic();
        System.out.println(response.orElse("No station found"));
    }

    static void getWayInstructionsMq() {
        getWayInstructionsLogic();
        ReceiverMq.fetchData();
    }

    static Optional<String> getClosestStation(String address) {
        final String r = iRoutingService.findClosestStation(address);
        if (r.equals("404")) {
            System.err.println("The service is not available in the current city");
            return Optional.empty();
        }
        System.err.printf("The closest station of %s is %s%n", address, r);
        return Optional.of(r);
    }

    static void findClosestStation() {
        System.out.println("Find the closest station");
        System.out.println("Enter an address: ");
        String address = sc.nextLine();
        getClosestStation(address).ifPresent(System.out::println);
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
        System.out.println("4: Get best itinerary instructions with activeMq");
        System.out.println("h: Show help");
        System.out.println("q: Quit");
        System.out.println("------------------------");
    }

    private static Optional<String> getWayInstructionsLogic() {
        System.out.println("Get best itinerary instructions");

        System.out.println("Enter a departure address: ");
        String fromAddress = sc.nextLine();
        String fromCoords = iRoutingService.getGPSCoordsFromAddress(fromAddress);

        Optional<String> startClosestStation = getClosestStation(fromAddress);
        if (startClosestStation.isEmpty()) return Optional.empty();

        System.out.println("Enter a destination address: ");
        String toAddress = sc.nextLine();
        String toCoords = iRoutingService.getGPSCoordsFromAddress(toAddress);

        Optional<String> endClosestStation = getClosestStation(toAddress);
        if (endClosestStation.isEmpty()) return Optional.empty();

        String response = iRoutingService.getWayInstructions(fromCoords, startClosestStation.get(), toCoords, endClosestStation.get());
        return Optional.of(response);
    }

}
