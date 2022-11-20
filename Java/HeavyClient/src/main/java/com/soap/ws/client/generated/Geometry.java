
package com.soap.ws.client.generated;

import javax.xml.bind.JAXBElement;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElementRef;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Classe Java pour Geometry complex type.
 * 
 * <p>Le fragment de schéma suivant indique le contenu attendu figurant dans cette classe.
 * 
 * <pre>
 * &lt;complexType name="Geometry"&gt;
 *   &lt;complexContent&gt;
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType"&gt;
 *       &lt;sequence&gt;
 *         &lt;element name="coordinates" type="{http://schemas.microsoft.com/2003/10/Serialization/Arrays}ArrayOfArrayOfdouble" minOccurs="0"/&gt;
 *         &lt;element name="type" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/&gt;
 *       &lt;/sequence&gt;
 *     &lt;/restriction&gt;
 *   &lt;/complexContent&gt;
 * &lt;/complexType&gt;
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "Geometry", propOrder = {
    "coordinates",
    "type"
})
public class Geometry {

    @XmlElementRef(name = "coordinates", namespace = "http://schemas.datacontract.org/2004/07/RoutingServer", type = JAXBElement.class, required = false)
    protected JAXBElement<ArrayOfArrayOfdouble> coordinates;
    @XmlElementRef(name = "type", namespace = "http://schemas.datacontract.org/2004/07/RoutingServer", type = JAXBElement.class, required = false)
    protected JAXBElement<String> type;

    /**
     * Obtient la valeur de la propriété coordinates.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link ArrayOfArrayOfdouble }{@code >}
     *     
     */
    public JAXBElement<ArrayOfArrayOfdouble> getCoordinates() {
        return coordinates;
    }

    /**
     * Définit la valeur de la propriété coordinates.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link ArrayOfArrayOfdouble }{@code >}
     *     
     */
    public void setCoordinates(JAXBElement<ArrayOfArrayOfdouble> value) {
        this.coordinates = value;
    }

    /**
     * Obtient la valeur de la propriété type.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public JAXBElement<String> getType() {
        return type;
    }

    /**
     * Définit la valeur de la propriété type.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public void setType(JAXBElement<String> value) {
        this.type = value;
    }

}
