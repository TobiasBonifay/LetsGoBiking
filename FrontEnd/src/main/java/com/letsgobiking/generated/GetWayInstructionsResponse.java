
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
 *         &lt;element name="GetWayInstructionsResult" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/&gt;
 *       &lt;/sequence&gt;
 *     &lt;/restriction&gt;
 *   &lt;/complexContent&gt;
 * &lt;/complexType&gt;
 * </pre>
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "", propOrder = {
        "getWayInstructionsResult"
})
@XmlRootElement(name = "GetWayInstructionsResponse")
public class GetWayInstructionsResponse {

    @XmlElementRef(name = "GetWayInstructionsResult", namespace = "http://tempuri.org/", type = JAXBElement.class, required = false)
    protected JAXBElement<String> getWayInstructionsResult;

    /**
     * Gets the value of the getWayInstructionsResult property.
     *
     * @return possible object is
     * {@link JAXBElement }{@code <}{@link String }{@code >}
     */
    public JAXBElement<String> getGetWayInstructionsResult() {
        return getWayInstructionsResult;
    }

    /**
     * Sets the value of the getWayInstructionsResult property.
     *
     * @param value allowed object is
     *              {@link JAXBElement }{@code <}{@link String }{@code >}
     */
    public void setGetWayInstructionsResult(JAXBElement<String> value) {
        this.getWayInstructionsResult = value;
    }

}
