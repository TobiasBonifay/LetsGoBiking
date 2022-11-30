package com.letsgobiking;

import com.letsgobiking.generated.IRoutingService;
import com.letsgobiking.generated.RoutingService;

import java.io.IOException;
import java.util.Scanner;

public class Main {

    private static final Scanner sc = new Scanner(System.in);

    public static void main(String[] args) {

        RoutingService routingService = new RoutingService();
        IRoutingService iRoutingService = routingService.getBasicHttpBindingIRoutingService();

        System.out.println("Hello !");

        System.out.println("Stations available:");
        System.out.println(iRoutingService.getStations());

        System.out.println("Closest station:");
        System.out.println(iRoutingService.findClosestStation());

        System.out.println("Please enter the starting point of your trip :");
        String adr1 = iRoutingService.getGPSCoordsFromAddress(sc.nextLine());
        System.out.println("Please enter the destination of your trip :");
        String adr2 = iRoutingService.getGPSCoordsFromAddress(sc.nextLine());

        System.out.println(iRoutingService.getWayInstructions(adr1, adr2));
    }
}
