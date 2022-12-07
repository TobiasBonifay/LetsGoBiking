
package com.letsgobiking.generated;

import javax.xml.bind.JAXBElement;
import javax.xml.bind.annotation.*;


/**
 * <p>Java class for anonymous complex type.
 *
 * <p>The following schema fragment specifies the expected content contained within this class.
 *
 * <pre>
 * &lt;complexType&gt;
 *   &lt;complexContent&gt;
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType"&gt;
 *       &lt;sequence&gt;
 *         &lt;element name="FindClosestStationResult" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/&gt;
 *       &lt;/sequence&gt;
 *     &lt;/restriction&gt;
 *   &lt;/complexContent&gt;
 * &lt;/complexType&gt;
 * </pre>
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "", propOrder = {
        "findClosestStationResult"
})
@XmlRootElement(name = "FindClosestStationResponse")
public class FindClosestStationResponse {

    @XmlElementRef(name = "FindClosestStationResult", namespace = "http://tempuri.org/", type = JAXBElement.class, required = false)
    protected JAXBElement<String> findClosestStationResult;

    /**
     * Gets the value of the findClosestStationResult property.
     *
     * @return possible object is
     * {@link JAXBElement }{@code <}{@link String }{@code >}
     */
    public JAXBElement<String> getFindClosestStationResult() {
        return findClosestStationResult;
    }

    /**
     * Sets the value of the findClosestStationResult property.
     *
     * @param value allowed object is
     *              {@link JAXBElement }{@code <}{@link String }{@code >}
     */
    public void setFindClosestStationResult(JAXBElement<String> value) {
        this.findClosestStationResult = value;
    }

}
