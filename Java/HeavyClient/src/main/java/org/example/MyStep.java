package org.example;

public class MyStep {
    protected Double distance;
    protected Double duration;
    protected String instruction;

    public Double getDistance() {
        return distance;
    }

    public void setDistance(Double distance) {
        this.distance = distance;
    }

    public Double getDuration() {
        return duration;
    }

    public void setDuration(Double duration) {
        this.duration = duration;
    }

    public String getInstruction() {
        return instruction;
    }

    public void setInstruction(String instruction) {
        this.instruction = instruction;
    }

    @Override
    public String toString() {
        return instruction + System.lineSeparator() +
                // round distance and duration to 2 decimals
                "distance : " + String.format("%.2f", distance) + " m" + System.lineSeparator() +
                "for " + String.format("%.2f", duration) + " s" + System.lineSeparator();
    }
}
