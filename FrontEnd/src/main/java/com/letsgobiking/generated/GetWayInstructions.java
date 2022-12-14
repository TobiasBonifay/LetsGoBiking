
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
 *         &lt;element name="fromCoords" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/&gt;
 *         &lt;element name="startClosesetStztion" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/&gt;
 *         &lt;element name="toCoords" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/&gt;
 *         &lt;element name="endClosestStation" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/&gt;
 *       &lt;/sequence&gt;
 *     &lt;/restriction&gt;
 *   &lt;/complexContent&gt;
 * &lt;/complexType&gt;
 * </pre>
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "", propOrder = {
        "fromCoords",
        "startClosesetStztion",
        "toCoords",
        "endClosestStation"
})
@XmlRootElement(name = "GetWayInstructions")
public class GetWayInstructions {

    @XmlElementRef(name = "fromCoords", namespace = "http://tempuri.org/", type = JAXBElement.class, required = false)
    protected JAXBElement<String> fromCoords;
    @XmlElementRef(name = "startClosesetStztion", namespace = "http://tempuri.org/", type = JAXBElement.class, required = false)
    protected JAXBElement<String> startClosesetStztion;
    @XmlElementRef(name = "toCoords", namespace = "http://tempuri.org/", type = JAXBElement.class, required = false)
    protected JAXBElement<String> toCoords;
    @XmlElementRef(name = "endClosestStation", namespace = "http://tempuri.org/", type = JAXBElement.class, required = false)
    protected JAXBElement<String> endClosestStation;

    /**
     * Gets the value of the fromCoords property.
     *
     * @return possible object is
     * {@link JAXBElement }{@code <}{@link String }{@code >}
     */
    public JAXBElement<String> getFromCoords() {
        return fromCoords;
    }

    /**
     * Sets the value of the fromCoords property.
     *
     * @param value allowed object is
     *              {@link JAXBElement }{@code <}{@link String }{@code >}
     */
    public void setFromCoords(JAXBElement<String> value) {
        this.fromCoords = value;
    }

    /**
     * Gets the value of the startClosesetStztion property.
     *
     * @return possible object is
     * {@link JAXBElement }{@code <}{@link String }{@code >}
     */
    public JAXBElement<String> getStartClosesetStztion() {
        return startClosesetStztion;
    }

    /**
     * Sets the value of the startClosesetStztion property.
     *
     * @param value allowed object is
     *              {@link JAXBElement }{@code <}{@link String }{@code >}
     */
    public void setStartClosesetStztion(JAXBElement<String> value) {
        this.startClosesetStztion = value;
    }

    /**
     * Gets the value of the toCoords property.
     *
     * @return possible object is
     * {@link JAXBElement }{@code <}{@link String }{@code >}
     */
    public JAXBElement<String> getToCoords() {
        return toCoords;
    }

    /**
     * Sets the value of the toCoords property.
     *
     * @param value allowed object is
     *              {@link JAXBElement }{@code <}{@link String }{@code >}
     */
    public void setToCoords(JAXBElement<String> value) {
        this.toCoords = value;
    }

    /**
     * Gets the value of the endClosestStation property.
     *
     * @return possible object is
     * {@link JAXBElement }{@code <}{@link String }{@code >}
     */
    public JAXBElement<String> getEndClosestStation() {
        return endClosestStation;
    }

    /**
     * Sets the value of the endClosestStation property.
     *
     * @param value allowed object is
     *              {@link JAXBElement }{@code <}{@link String }{@code >}
     */
    public void setEndClosestStation(JAXBElement<String> value) {
        this.endClosestStation = value;
    }

}
